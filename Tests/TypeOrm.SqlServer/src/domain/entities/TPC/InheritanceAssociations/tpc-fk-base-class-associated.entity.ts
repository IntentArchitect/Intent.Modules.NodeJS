import { TpcFkBaseClass } from './tpc-fk-base-class.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tpc_fk_base_class_associated')
export class TpcFkBaseClassAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  fkBaseClassCompositeKeyA: string;

  @Column()
  fkBaseClassCompositeKeyB: string;

  @ManyToOne(() => TpcFkBaseClass, { cascade: ['insert', 'update'], nullable: false })
  fkBaseClass: TpcFkBaseClass;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}