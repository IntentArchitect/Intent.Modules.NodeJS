import { PK_A_CompositeKey } from './pk-a-composite-key.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('fk_a_composite_foreign_key')
export class FK_A_CompositeForeignKey {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  pkACompositekeyCompositeKeyA: string;

  @Column()
  pkACompositekeyCompositeKeyB: string;

  @ManyToOne(() => PK_A_CompositeKey, { cascade: ['insert', 'update'], nullable: false })
  pK_A_CompositeKey: PK_A_CompositeKey;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}