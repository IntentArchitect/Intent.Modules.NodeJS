import { TpcFkDerivedClass } from './tpc-fk-derived-class.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tpc_fk_associated_class')
export class TpcFkAssociatedClass {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  fkDerivedClassCompositeKeyA: string;

  @Column()
  fkDerivedClassCompositeKeyB: string;

  @ManyToOne(() => TpcFkDerivedClass, { cascade: ['insert', 'update'], nullable: false })
  fkDerivedClass: TpcFkDerivedClass;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}